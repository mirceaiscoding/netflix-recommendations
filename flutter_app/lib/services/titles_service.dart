import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/genre_update_model.dart';
import 'package:flutter_app/models/title_model.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';
import 'package:flutter_app/services/secure_storage.dart';
import 'package:http/http.dart' as http;

class TitlesService {
  static final TitlesService _instance = TitlesService._internal();

  TitlesService._internal();

  List<WatcherTitlePreferenceModel> watcherTitleModels = [];
  List<WatcherTitlePreferenceModel> originalWatcherTitleModels = [];

  factory TitlesService() {
    return _instance;
  }

  Future<int> load() async {
    if (kDebugMode) {
      print("Loading titles for user...");
    }
    var res = await getTitles();
    if (res.statusCode == 200) {
      var titleModels = titlesFromJson(res.body);
      for (var titleModel in titleModels) {
        // if (kDebugMode) {
        //   print(titleModel);
        // }
        WatcherTitlePreferenceModel? watcherTitlePreferenceModel =
            await getPreferenceModel(titleModel);
        if (watcherTitlePreferenceModel != null) {
          watcherTitleModels.add(watcherTitlePreferenceModel.clone());
          originalWatcherTitleModels.add(watcherTitlePreferenceModel.clone());
        }
      }
    } else {
      print("Error ${res.statusCode} loading titles for user");
    }
    if (kDebugMode) {
      print("Done loading titles");
    }
    return res.statusCode;
  }

  static Future<http.Response> getTitles(
      {bool sorted = false, int filterFlags = 0, int page = 1}) async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    var res = await http.get(
        Uri.parse(kTitleURL + "GetAllFromPage/" + page.toString()),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer ' + authModel.accessToken,
          'orderByFlagsPacked': sorted ? 1.toString() : 0.toString(),
          'whereFlagsPacked': filterFlags.toString(),
        });

    return res;
  }

  Future<http.Response> batchCreateOrUpdateTitles() async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    if (kDebugMode) {
      print("Sending update title preferences request...");
    }

    var res = await http.post(
      Uri.parse(kWatcherTitleURL + "BatchCreateOrUpdate"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
        'Authorization': 'Bearer ' + authModel.accessToken
      },
      body: jsonEncode(watcherTitleModels),
    );
    return res;
  }

  Future<http.Response> batchCreateOrUpdateGenres() async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    if (kDebugMode) {
      print("Sending update genres request...");
    }

    var res = await http.put(
      Uri.parse(kWatcherGenresURL + "BatchAddToScore"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
        'Authorization': 'Bearer ' + authModel.accessToken
      },
      body: jsonEncode(getGenreChanges()),
    );
    return res;
  }

  double getPreferanceScore(WatcherTitlePreferenceModel title) {
    double score = 0;
    if (title.watchLater) {
      score++;
    }
    if (title.preference == 1) {
      score++;
    }
    if (title.preference == 2) {
      score--;
    }
    return score;
  }

  List<GenreUpdateModel> getGenreChanges() {
    Map<int, double> genreChanges = {};

    for (int i = 0; i < watcherTitleModels.length; i++) {
      var original = originalWatcherTitleModels[i];
      var current = watcherTitleModels[i];
      var scoreChange =
          getPreferanceScore(current) - getPreferanceScore(original);
      if (scoreChange != 0) {
        if (kDebugMode) {
          print(
              "Genre score change! Original [${original}], Current [${current}]");
        }
        for (var genre in original.watcherGenreModels) {
          genreChanges.update(genre.genreId, (value) => value + scoreChange,
              ifAbsent: () => scoreChange);
        }
      }
    }

    print(genreChanges);

    var genreChangesList = genreChanges.entries
        .map(
            (x) => GenreUpdateModel(genreId: x.key, watcherGenreScore: x.value))
        .where((x) => x.watcherGenreScore != 0)
        .toList();

    if (kDebugMode) {
      print(genreChangesList);
    }
    return genreChangesList;
  }

  Future batchUpdate() async {
    if (kDebugMode) {
      print("Updating database...");
    }

    await batchCreateOrUpdateTitles();
    await batchCreateOrUpdateGenres();

    // Deep copy from current to original
    originalWatcherTitleModels =
        watcherTitleModels.map((e) => e.clone()).toList();
  }

  static Future<WatcherTitlePreferenceModel?> getPreferenceModel(
      TitleModel titleModel) async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    var res = await http.get(
        Uri.parse(
            kWatcherTitleURL + "GetOneById/" + titleModel.netflixId.toString()),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer ' + authModel.accessToken
        });

    if (res.statusCode == 200) {
      var response = jsonDecode(res.body);
      return WatcherTitlePreferenceModel.fromJson(response);
    } else {
      return null;
    }
  }
}
