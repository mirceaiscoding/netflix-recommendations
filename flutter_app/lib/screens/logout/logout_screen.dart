import 'package:flutter/material.dart';
import 'package:flutter_app/components/menu_drawer.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/watchlist/components/body.dart';

class LogoutScreen extends StatelessWidget {


  const LogoutScreen({Key? key}) : super(key: key);
  
  @override
  Widget build(BuildContext context) {
    var currentUser = "user";
    var currentCountry = "Romania";
    Size size = MediaQuery.of(context).size; // Screen size
    return Scaffold(
      drawerEnableOpenDragGesture: false,
      drawerEdgeDragWidth: 0,
      appBar: buildAppBar(),
      endDrawer: const MenuDrawer(),
      body:Stack(
      children:[
        Positioned(
          left: size.width*0.1,
          top: size.height*0.1,
          child:Container(
            width: size.width * 0.8,
            height: size.height * 0.45,
            margin: const EdgeInsets.only(bottom: 10.0),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(kImageBoarderRadius),
              border: Border.all(
                color: kPrimaryColor,
                width: 2,
              )
            ),
            child:Column(
              children: [
                const SizedBox(height: 50,),
                const ListTile(
                  title: Text('Username:',style: kLogoutTitleStyle),
                ),
                Padding(
                  padding: const EdgeInsets.only(left: 50),
                  child: Container(
                    alignment: Alignment.bottomLeft,
                    child: Text(currentUser,style: kLogoutLinetyle),)
                )
                ,

                const SizedBox(height: 40,),
                const ListTile(
                  title: Text('Country:',style: kLogoutTitleStyle),
                ),
                Padding(
                  padding: const EdgeInsets.only(left: 50),
                  child: Container(
                    alignment: Alignment.bottomLeft,
                    child: Text(currentCountry,style: kLogoutLinetyle),)
                ),

                const SizedBox(height: 40,),
                ElevatedButton(
                  style: ElevatedButton.styleFrom(
                  primary: kPrimaryColor,
                  ),
                  onPressed: () {
                    Navigator.pushNamed(context, '/login');
                  },
                  child: const Text('LOGOUT',style: kButtonTextStyle,),
                )
              ],
            )
          ),
      ),
        Positioned(
            left: size.width * 0.5-50,
            top: size.height*0.1-50,
            child: Icon(Icons.account_circle,size: 100,color: kPrimaryColor,)
        ),
      ]
      )
    );
  }
}

AppBar buildAppBar() {
  return AppBar(
    backgroundColor: kAppBarColor,
    shadowColor: Colors.black,
    iconTheme: const IconThemeData(
      color: kPrimaryColor,
      size: 40,
    ),
  );
}