import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class Body extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Size of the screen
    return Column(
      children: <Widget>[
        // Movie poster
        Center(
          child: Container(
            width: size.width * 0.7, // 70% width
            padding: const EdgeInsets.all(kDefaultPadding), // Padding
            decoration: BoxDecoration(
                // borderRadius: BorderRadius.circular(kImageBoarderRadius),
                boxShadow: [
                  BoxShadow(
                      offset: Offset(-10, 5),
                      blurRadius: 100,
                      spreadRadius: -10,
                      color: kImageBorderColor.withOpacity(0.1),
                      ),
                ]),
            child: ClipRRect(
              borderRadius: BorderRadius.circular(kImageBoarderRadius),
              child: Image.asset(
                "assets/images/bumblebee_poster.jpg", // Movie poster image
              ),
            ),
          ),
        ),

        // Movie title
      ],
    );
  }
}
