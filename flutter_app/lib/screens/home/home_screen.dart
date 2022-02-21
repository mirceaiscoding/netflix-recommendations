import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/components/body.dart';

class Homescreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(),
      body: Body(),
      bottomNavigationBar: Container(
        color: kPrimaryColor,
        height: 80.0,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: <Widget>[
            IconButton(
              onPressed: nextMovie(),
              iconSize: 40,
              icon: const Icon(
                Icons.arrow_back_ios_outlined,
                color: kIconColor,
              ),
            ),
            IconButton(
              onPressed: dislikeMovie(),
              iconSize: 40,
              icon: const Icon(
                Icons.sentiment_dissatisfied_outlined,
                color: kIconColor,
              ),
            ),
            IconButton(
              onPressed: addToWatchlist(),
              iconSize: 50,
              icon: const Icon(
                Icons.add_circle,
                color: kIconColor,
              ),
            ),
            IconButton(
              onPressed: likeMovie(),
              iconSize: 40,
              icon: const Icon(
                Icons.sentiment_satisfied_outlined,
                color: kIconColor,
              ),
            ),
            IconButton(
              onPressed: nextMovie(),
              iconSize: 40,
              icon: const Icon(
                Icons.arrow_forward_ios_outlined,
                color: kIconColor,
              ),
            ),
          ],
        ),
      ),
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

  dislikeMovie() {
    // TODO
  }

  likeMovie() {
    // TODO
  }

  nextMovie() {
    // TODO
  }

  previousMovie() {
    // TODO
  }

  addToWatchlist() {
    // TODO
  }
}
