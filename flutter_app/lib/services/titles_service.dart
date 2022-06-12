import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/title_model.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';
import 'package:flutter_app/services/secure_storage.dart';
import 'package:http/http.dart' as http;

class TitlesService {
  static final TitlesService _instance = TitlesService._internal();

  TitlesService._internal();

  List<WatcherTitlePreferenceModel> watcherTitleModels = [];

  factory TitlesService() {
    return _instance;
  }

  Future<int> load() async {
    if (kDebugMode) {
      print("Loading titles for user...");
    }
    var res = await getBestTitles(1);
    if (res.statusCode == 200) {
      var titleModels = titlesFromJson(res.body);
      for (var titleModel in titleModels) {
        if (kDebugMode) {
          print(titleModel);
        }
        WatcherTitlePreferenceModel? watcherTitlePreferenceModel =
            await getPreferenceModel(titleModel);
        if (watcherTitlePreferenceModel != null) {
          watcherTitleModels.add(watcherTitlePreferenceModel);
        }
      }
    } else {
      print("Error ${res.statusCode} loading titles for user");
    }
    return res.statusCode;
  }

  Future<http.Response> getBestTitles(int page) async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    var res = await http.get(
        Uri.parse(kTitleURL + "GetAllFromPage/" + page.toString()),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer ' + authModel.accessToken
        });

    return res;
  }

  static List<WatcherTitlePreferenceModel> models = [];

  Future<http.Response> batchCreateOrUpdate() async {
    // service for secure storage
    final SecureStorage _secureStorage = SecureStorage();

    var authModel = await _secureStorage.readAuthModel();

    var res = await http.post(
      Uri.parse(kWatcherTitleURL + "BatchCreateOrUpdate"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
        'Authorization': 'Bearer ' + authModel.accessToken
      },
      body: jsonEncode(models),
    );
    return res;
  }

  Future<WatcherTitlePreferenceModel?> getPreferenceModel(
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
