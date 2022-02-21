import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/components/body.dart';

class Homescreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(),
      body: Body(),
    );
  }

  AppBar buildAppBar() {
    return AppBar(
      backgroundColor: kAppBarColor,
      shadowColor: Colors.black,
      actions: <Widget>[
        IconButton(
          icon: const Icon(
            Icons.menu,
            color: kPrimaryColor,
          ),
          iconSize: 40.0,
          onPressed: openMenu(),
        )
      ],
    );
  }

  openMenu() {
    // TODO: Open menu
  }
}
