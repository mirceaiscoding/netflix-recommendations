import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class Body extends StatelessWidget {

  var movie ={
    'title': 'Bumblebee',
    'year': '2018',
    'score': '6.7',
    'number_of_reviews': '156k',
    'description': 'On the run in the year 1987, Bumblebee finds refuge in a junkyard in a small California beach town. On the cusp of turning 18 and trying to find her place in the world, Charlie Watson discovers Bumblebee, battle-scarred and broken.',
  };

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Size of the screen
    return Column(
      children: <Widget>[
        // Movie poster
        Center(
          child: Container(
            width: size.width * 0.7, // 70% width
            margin: const EdgeInsets.all(kDefaultPadding), // Padding
            decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(kImageBoarderRadius),
                border: Border.all(
                  color: kImageBorderColor,
                  width: 2,
                )),
            child: ClipRRect(
              borderRadius: BorderRadius.circular(kImageBoarderRadius),
              child: Image.asset(
                "assets/images/bumblebee_poster.jpg", // Movie poster image
              ),
            ),
          ),
        ),

        // Movie title
        Align(
          child: RichText(
            text: TextSpan(
              text: "${movie['title']}  (${movie['year']})",
              style: kMovieTitleStyle
            ),
          ),
        ),

        // IMDB score and number of reviews


      ],
    );
  }
}
