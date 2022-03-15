import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_app/screens/home/home_screen.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('menu drawer tests', () {
    testWidgets('open menu', (WidgetTester tester) async {
      // Build an App with a Text widget that displays the letter 'H'.
      await tester.pumpWidget(MaterialApp(
        debugShowCheckedModeBanner: false, // Remove 'debug' label from app
        title: 'Netflix Recommendations',
        initialRoute: '/home', // Route when the app opens
        routes: {
          '/home': (context) => const Homescreen(),
        },
      ));
      await tester.pumpAndSettle();

      // Search for the menu button
      var menuButton = find.byIcon(Icons.menu);
      await tester.pumpAndSettle();
      expect(menuButton, findsOneWidget);

      // Emulate a tap on the menu button
      await tester.tap(menuButton);
      await tester.pumpAndSettle();

      // Verify the menu is open
      expect(find.text('ACCOUNT'), findsOneWidget);
    });
  });
}
