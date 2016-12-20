var passport = require('passport');
var mongoose = require('mongoose');
var User = mongoose.model('User');

var sendJSONresponse = function (res, status, content) {
    res.status(status);
    res.json(content);
};

module.exports.register = function (req, res) {

    var user = new User();

    user.name = req.body.name;
    user.email = req.body.email;
    user.role = req.body.role;
    user.setPassword(req.body.password);
    console.log(JSON.stringify(user));
    user.save(function (err) {
        var token;
        token = user.generateJwt();
        res.status(200);
        res.json({
            "token": token,
            "role": user.role
        });
    });

};

module.exports.login = function (req, res) {

    // if(!req.body.email || !req.body.password) {
    //   sendJSONresponse(res, 400, {
    //     "message": "All fields required"
    //   });
    //   return;
    // }

    passport.authenticate('local', function (err, user, info) {
        var token;

        // If Passport throws/catches an error
        if (err) {
            res.status(404).json(err);
            return;
        }

        // If a user is found
        if (user) {
            token = user.generateJwt();
            res.status(200);
            res.json({
                "token": token,
                "role": user.role
            });
        } else {
            // If user is not found
            res.status(401).json(info);
        }
    })(req, res);
};

//// check the role of the user
//module.exports.checkrole = function (req, res) {
//    console.log('role');
//    var isAdmin = false;
//    if (!req.payload.role == 'Admin') {
//        isAdmin = true;
//    }
//    console.log('Role : ' + req.payload.role);
//    res.status(200);
//    res.json({
//        "IsAdmin": isAdmin
//    });
//};