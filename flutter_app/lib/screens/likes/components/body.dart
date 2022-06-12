import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class Body extends StatelessWidget {

  // Constructor
  // Used like this: Body(movie=...)
  final movie;
  const Body({Key? key, @required this.movie}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Size of the screen
    return Center(
      child: SizedBox(
        width: size.width * 1,
        height: size.height * 0.25,
        child: Padding(
          padding: const EdgeInsets.only(left: kDefaultPadding, top: kDefaultPadding), 
          //padding: const EdgeInsets.all(kDefaultPadding),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              // Movie poster
              Center(
                child: Container(
                  

                  child: Image.network(
                    movie['poster']// Movie poster image
                  ),
                 
                ),
              ),

              SizedBox(width: 20,),

              // Movie info
              Expanded(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  // Movie title
                  children: <Widget>[
                    RichText(
                      overflow: TextOverflow.ellipsis,
                      maxLines: 5, // has to be changed depending on fontSize
                      text: TextSpan(
                        text: "${movie['title']} (${movie['year']})",
                        style: TextStyle(
                        fontFamily: "Work Sans",
                        fontSize: 22.0,
                        )
                      ),
                    
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
                                text:
                                    " ${movie['score']} (${movie['number_of_reviews']})",
                                style: kIMDBRatingStyle),
                          ),
                      ],
                    ),
                  )
                  ]
                )
              ),
              // Options button
              GestureDetector(
                child: Icon(Icons.more_vert, color: kPrimaryColor,),
                onTap:(){
                  showModalBottomSheet<dynamic>(
                    backgroundColor: kBackgroundColor,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.vertical(
                        top: Radius.circular(20)
                        )
                      ),
                    context: context,
                    builder: (context) => Wrap(
                      
                      children: <Widget>[
                        ListTile(
                          title: const Center(
                              child: Text(
                            'Rate movie',
                            style: kMenuItemTextStyle,
                          )),
                          onTap: () {
                            Navigator.pop(context);
                          },
                        ),
                        ListTile(
                          title: const Center(
                              child: Text(
                            'Add to Watchlist',
                            style: kMenuItemTextStyle,
                          )),
                          onTap: () {
                            Navigator.pop(context);
                          },
                        ),
                        ListTile(
                          title: const Center(
                              child: Text(
                            'Add to Disliked movies',
                            style: kMenuItemTextStyle,
                          )),
                          onTap: () {
                            Navigator.pop(context);
                          },
                        ),
                      ]
                      )
                  );
                }
              )    
                    
            ],
          ),
        ),
      ),
    );
  }
}
