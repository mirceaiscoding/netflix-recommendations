import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter_app/components/menu_drawer.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/components/body.dart';
import 'package:flutter_app/services/auth_service.dart';
import 'package:flutter_app/services/titles_service.dart';

import 'components/bottom_actions.dart';

class Homescreen extends StatefulWidget {
  const Homescreen({Key? key}) : super(key: key);

  @override
  HomescreenState createState() => HomescreenState();
}

class HomescreenState extends State<Homescreen> {
  int currentPage = -1;
  PageController pageController = PageController(initialPage: -1);

  final _scaffoldKey = GlobalKey<ScaffoldState>();

  final TitlesService _titlesService = TitlesService();

  final AuthService _authService = AuthService();

  static bool loading = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawerEnableOpenDragGesture: false,
      drawerEdgeDragWidth: 0,
      key: _scaffoldKey,
      appBar: buildAppBar(),
      endDrawer: const MenuDrawer(),
      body: PageView.builder(
        physics: const BouncingScrollPhysics(),
        controller: pageController,
        // allowImplicitScrolling: true, // preloads pages
        onPageChanged: (int val) => setPage(val),
        itemBuilder: (context, index) {
          if (index > _titlesService.watcherTitleModels.length) {
            index = _titlesService.watcherTitleModels.length;
            scrollTo(index);
          }
          if (index > _titlesService.watcherTitleModels.length - 1) {
            loadTitles();
            return const Center(child: CircularProgressIndicator());
          }
          return Scaffold(
            body: Body(title: _titlesService.watcherTitleModels[index]),
            bottomNavigationBar: BottomActions(
                null,
                (int val) => changePage(val),
                _titlesService.watcherTitleModels[index]),
          );
        },
      ),
    );
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

  // Changes the current page by val
  // -1 if back or 1 if forward
  changePage(int val) {
    // print("Change page with $val");
    setState(() {
      currentPage = max(0, currentPage + val);
      currentPage = min(currentPage, _titlesService.watcherTitleModels.length);
    });

    scrollToCurrentPage(); // Animate scrolling to new current page
  }

  // Scroll animation to the current page
  scrollToCurrentPage() {
    pageController.animateToPage(currentPage,
        duration: const Duration(milliseconds: 200), curve: Curves.bounceInOut);
  }

  // Scroll animation to the current page
  scrollTo(int index) {
    pageController.animateToPage(index,
        duration: const Duration(milliseconds: 200), curve: Curves.bounceInOut);
  }

  // Sets the page to a specified value
  // It is called by swiping so no animation is required
  setPage(int val) {
    // print("Set page to $val");
    setState(() {
      currentPage = val;
    });
  }

  loadTitles() {
    if (loading == false) {
      loading = true;
      _titlesService.load().then((status) => {afterLoadIsDone(status)});
    }
  }

  afterLoadIsDone(int status) {
    loading = false;
    if (status == 401) {
      // Unauthorized -> Try to refresh
      _authService.refreshAccessToken().then((success) => {
            if (success == false)
              {
                // Go back to login
                Navigator.pushNamed(context, "/login")
              }
            else
              // Try again to load with the refreshed token
              {loadTitles()}
          });
    } else {
      changePage(1);
      scrollToCurrentPage();
    }
  }
}
