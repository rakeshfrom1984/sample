var express = require('express');
var router = express.Router();
var jwt = require('express-jwt');
var auth = jwt({
  secret: 'MY_SECRET',
  userProperty: 'payload'
});

var ctrlProfile = require('../controllers/profile');
var ctrlAuth = require('../controllers/authentication');
var ctrlTask = require('../controllers/task');

// profile
router.get('/profile', auth, ctrlProfile.profileRead);

// authentication
router.post('/register', ctrlAuth.register);
router.post('/login', ctrlAuth.login);

// save the tasks
router.post('/task', auth, ctrlTask.addTask);
// tasks list route
router.get('/task', auth, ctrlTask.getList);

// Update task
router.put('/task', auth, ctrlTask.updateTask);
// checkrole task
//router.get('/checkrole', auth, ctrlAuth.checkrole);

module.exports = router;
