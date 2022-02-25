import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/login/components/login_form.dart';

class Body extends StatelessWidget {
  const Body({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Screen size

    // return Container(
    //   margin: EdgeInsets.symmetric(vertical: 10, horizontal: 10),
    //   width: size.width * 0.8, // 80% width
    //   child: ClipRRect(
    //     borderRadius: BorderRadius.circular(29),
    //     child: ElevatedButton(
    //       child: Text(
    //         "Login",
    //         style: kAuthStyle,
    //       ),
    //       onPressed: () => {},
    //       style: ElevatedButton.styleFrom(
    //         primary: kPrimaryColor,
    //       ),
    //     ),
    //   ),
    // );

    return LoginForm();
  }
}
