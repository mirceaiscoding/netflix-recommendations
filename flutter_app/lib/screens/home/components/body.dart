import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';

class Body extends StatelessWidget {
  // Constructor
  // Used like this: Body(movie=...)
  final WatcherTitlePreferenceModel title;
  const Body({Key? key, required this.title}) : super(key: key);

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
                    child: AspectRatio(
                      aspectRatio: 2 / 3,
                      child: Container(
                        decoration: BoxDecoration(
                            image: DecorationImage(
                          fit: BoxFit.fitWidth,
                          alignment: FractionalOffset.topCenter,
                          image: NetworkImage(title.poster),
                        )),
                      ),
                    ),
                    // child: Image.network(
                    //   title.poster, // Movie poster image
                    // ),
                  ),
                ),
              ),

              // Movie title
              RichText(
                text: TextSpan(
                    text: "${title.title} (${title.year})",
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
                      // Standard scale for posters
                      width: 40.0,
                      height: 27.0,
                      child: Image.asset("assets/icons/IMDB_logo.png"),
                    ), // IMDB logo
                    RichText(
                      text: TextSpan(
                          text: " ${title.rating} ", style: kIMDBRatingStyle),
                    ),
                  ],
                ),
              ),

              // Movie description
              RichText(
                text: TextSpan(
                    text: title.synopsis, style: kMovieDescriptionStyle),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
