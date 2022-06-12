import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/auth_model.dart';
import 'package:flutter_app/services/secure_storage.dart';
import 'package:http/http.dart' as http;

class AuthService {
  Future<http.Response> register(
      String username, String email, String password) async {
    var res = await http.post(
      Uri.parse(kAuthRequestURL + "SignupUser"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'userName': username,
        'email': email,
        'password': password,
      }),
    );
    return res;
  }

  Future<http.Response> login(String emailOrUsername, String password) async {
    var res = await http.post(
      Uri.parse(kAuthRequestURL + "Login"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'emailOrName': emailOrUsername,
        'password': password,
      }),
    );
    return res;
  }

  Future logout() async {
    var secureStorage = SecureStorage();
    await secureStorage.removeAuthModel();
  }

  Future<bool> refreshAccessToken() async {
    var secureStorage = SecureStorage();
    var authModel = await secureStorage.readAuthModel();

    var res = await http.post(
      Uri.parse(kTokensURL + "Refresh"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(authModel),
    );

    if (kDebugMode) {
      print("Refreshed token => statusCode ${res.statusCode}");
    }

    if (res.statusCode != 200) {
      return false;
    } else {
      var response = jsonDecode(res.body);
      var newAuthModel = AuthModel.fromJson(response);
      await secureStorage.writeAuthModel(newAuthModel);
      return true;
    }
  }
}
