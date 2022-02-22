import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/components/body.dart';

import 'components/bottom_actions.dart';

class Homescreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: buildAppBar(),
      body: PageView(
        children: const <Body>[
          Body(movie: {
            'title': 'Bumblebee',
            'year': '2018',
            'score': '6.7',
            'number_of_reviews': '156k',
            'description':
                "On the run in the year 1987, Bumblebee finds refuge in a junkyard in a small California beach town. On the cusp of turning 18 and trying to find her place in the world, Charlie Watson discovers Bumblebee, battle-scarred and broken.",
            'poster' : "https://m.media-amazon.com/images/M/MV5BMjUwNjU5NDMyNF5BMl5BanBnXkFtZTgwNzgxNjM2NzM@._V1_.jpg",
          }),
          Body(movie: {
            'title': 'Cars 3',
            'year': '2017',
            'score': '6.7',
            'number_of_reviews': '91k',
            'description':
                "Lightning McQueen sets out to prove to a new generation of racers that he's still the best race car in the world.",
            'poster' : "https://cdn.europosters.eu/image/750/cars-3-duel-i97645.jpg",
          }),
        ],
      ),
      bottomNavigationBar: BottomActions(),
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
