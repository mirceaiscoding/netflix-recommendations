import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/title_model.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';
import 'package:flutter_app/services/secure_storage.dart';
import 'package:http/http.dart' as http;

class TitlesService {
  static final TitlesService _instance = TitlesService._internal();

  TitlesService._internal();

  List<TitleModel> titleModels = [];

  factory TitlesService() {
    return _instance;
  }

  Future load() async {
    if (kDebugMode) {
      print("Loading titles for user...");
    }
    var res = await getBestTitles(1);
    if (res.statusCode == 200) {
      titleModels.addAll(titlesFromJson(res.body));
    } else {
      print("Error ${res.statusCode} loading titles for user");
    }
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
}
