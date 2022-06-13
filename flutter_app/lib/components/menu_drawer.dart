import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:path/path.dart';

class MenuDrawer extends StatelessWidget {
  const MenuDrawer({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    const menu = ['HOME', 'ACCOUNT', 'WATCHLIST', 'LIKES', 'DISLIKES'];
    const paths = ['/home', '/logout', '/watchlist', '/likes', '/dislikes'];
    return Drawer(
      backgroundColor: kBackgroundColor,
      // ListView allows scrolling inside the drawer
      child: ListView.builder(
        padding: EdgeInsets.zero,
        itemCount: menu.length+1,
          itemBuilder: (context, index) {
            if (index == 0){
              return const SizedBox(height: 50,);
            }
            return Container (
              color: ModalRoute.of(context)!.settings.name == paths[index-1] ? kCurrentMenuItem : Colors.transparent,
              child:ListTile(
                      title: Center(
                      child: Text(menu[index-1],style:kMenuItemTextStyle,)
                      ),
                      onTap: () {
                        Navigator.pushNamed(context, paths[index-1]);
                      },
              )
            );
          }
      )
    );
  }
}
      