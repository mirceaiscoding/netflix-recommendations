import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class Body extends StatelessWidget {
  var movie = {
    'title': 'Bumblebee',
    'year': '2018',
    'score': '6.7',
    'number_of_reviews': '156k',
    'description':
        'On the run in the year 1987, Bumblebee finds refuge in a junkyard in a small California beach town. On the cusp of turning 18 and trying to find her place in the world, Charlie Watson discovers Bumblebee, battle-scarred and broken.',
  };

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Size of the screen
    return Center(
      child: SizedBox(
        width: size.width * 0.8, // 80% width,
        child: Padding(
          padding: const EdgeInsets.all(kDefaultPadding),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              // Movie poster
              Center(
                child: Container(
                  margin: const EdgeInsets.only(bottom: 10.0),
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
              RichText(
                text: TextSpan(
                    text: "${movie['title']} (${movie['year']})",
                    style: kMovieTitleStyle),
              ),

              // IMDB score and number of reviews
              Padding(
                padding: const EdgeInsets.only(
                  top: kDefaultPadding * 0.2,
                  bottom: kDefaultPadding * 0.3,
                ),
                child: Row(
                  children: [
                    SizedBox(
                      width: 40.0,
                      child: Image.asset("assets/icons/IMDB_logo.png"),
                    ), // IMDB logo
                    RichText(
                      text: TextSpan(
                          text:
                              " ${movie['score']} (${movie['number_of_reviews']})",
                          style: kIMDBRatingStyle),
                    ),
                  ],
                ),
              ),

              // Movie description
              RichText(
                text: TextSpan(
                    text: "${movie['description']}",
                    style: kMovieDescriptionStyle),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
