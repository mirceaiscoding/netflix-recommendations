import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';
import 'package:flutter_app/services/secure_storage.dart';
import 'package:http/http.dart' as http;

class WatcherTitlePreferenceService {
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
}
