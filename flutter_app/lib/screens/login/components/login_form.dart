import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class LoginForm extends StatefulWidget {
  const LoginForm({Key? key}) : super(key: key);

  @override
  _LoginFormState createState() => _LoginFormState();
}

class _LoginFormState extends State<LoginForm> {
  // global key for this form
  final _formKey = GlobalKey<FormState>();

  // if the password is visible or not
  bool _isPasswordVisible = false;

  @override
  Widget build(BuildContext context) {
    return Form(
      key: _formKey,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          // Email field
          Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: kDefaultPadding,
            ),
            child: TextFormField(
              decoration: const InputDecoration(
                enabledBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: kFormFieldColor,
                  ),
                ),
                focusedBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: kFormFieldColor,
                  ),
                ),
                border: OutlineInputBorder(),
                labelStyle: kAuthStyle,
                hintStyle: kAuthStyle,
                labelText: 'Email',
                hintText: 'Enter your email',
              ),
            ),
          ),

          // Password field
          Padding(
            padding: const EdgeInsets.all(kDefaultPadding),
            child: TextFormField(
              // Form field aspect
              decoration: InputDecoration(
                enabledBorder: const OutlineInputBorder(
                  borderSide: BorderSide(
                    color: kFormFieldColor,
                  ),
                ),
                focusedBorder: const OutlineInputBorder(
                  borderSide: BorderSide(
                    color: kFormFieldColor,
                  ),
                ),
                border: const OutlineInputBorder(),
                labelStyle: kAuthStyle,
                hintStyle: kAuthStyle,
                labelText: 'Password',
                hintText: 'Enter your password',
                suffixIcon: IconButton(
                    // Toggle if password is visible or not
                    onPressed: () {
                      setState(() {
                        _isPasswordVisible = !_isPasswordVisible;
                      });
                    },
                    // Icon based on the state of _isPasswordVisible
                    icon: Icon(
                      _isPasswordVisible
                          ? Icons.visibility_off
                          : Icons.visibility,
                      color: kFormFieldIconColor,
                    )),
              ),
              obscureText: !_isPasswordVisible,
            ),
          ),

          // Submit button
          ElevatedButton(
            style: ElevatedButton.styleFrom(
              primary: kPrimaryColor,
            ),
            onPressed: () {
              // Check if the form is valid
              if (_formKey.currentState!.validate()) {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(content: Text('Logging in...')),
                );
              }
            },
            child: const Text(
              'Login',
              style: kButtonTextStyle,
            ),
          ),
        ],
      ),
    );
  }
}
