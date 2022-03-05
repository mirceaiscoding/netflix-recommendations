import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:http/http.dart' as http;

class AuthService {
  Future<http.Response> register(String email, String password) async {
    var res = await http.post(
      Uri.parse(kAuthRequestURL + "Signup"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
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
}
