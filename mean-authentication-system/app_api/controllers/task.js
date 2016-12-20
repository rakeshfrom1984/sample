var mongoose = require('mongoose');
var Task = mongoose.model('Task');


// Saving the task
module.exports.addTask = function (req, res) {
    console.log('=========================================================');
    console.log(JSON.stringify(req.payload))
    
    if (!req.payload._id || req.payload.role == 'Developer') {
        console.log('========================================================= no permssion');
        res.status(401).json({
            "message": "UnauthorizedError: has no permission"
        });
    }
    else {
        console.log('========================================================= in else');
        var task = new Task();
        task.userid = req.payload._id;
        task.title = req.body.title;
        task.description = req.body.description;
        task.created_date = new Date().getDate();

        console.log(JSON.stringify(task));
        task.save(function (err) {
            console.log('========================================================= saved');
            console.log(JSON.stringify(err));
            res.status(200).json(task);
        });
    }

};
// getting the list of the task
module.exports.getList = function (req, res) {
    console.log('get lsit called')
    console.log(JSON.stringify(req.payload))

    if (!req.payload._id) {
        console.log('get list id not found')
        res.status(401).json({
            "message": "UnauthorizedError: has no permission"
        });
    }
    else {
        console.log('get list id found')
        //var task = new Task();

        //task.user_id = req.payload._id;
        //task.title = req.body.title;
        //task.description = req.body.description;
        //task.created_date = new Date().getDate();

        //console.log(JSON.stringify(task));
        Task.find({}, function (err, tasks) {
            //var userMap = {};

            //users.forEach(function (user) {
            //    userMap[user._id] = user;
            //});

            //res.send(userMap);
            console.log('get list list of tasks')
            res.status(200).json(tasks);
        });
    }

};

// get task by Id
// update task

module.exports.updateTask = function (req, res) {
    console.log(JSON.stringify(req.payload))

    if (!req.payload._id || req.payload.role == 'Developer') {
        res.status(401).json({
            "message": "UnauthorizedError: has no permission"
        });
    }
    else {
        var task = new Task();
        task._id = req.body._id;
        task.title = req.body.title;
        task.description = req.body.description;
        //task.created_date = new Date().getDate();

        console.log(JSON.stringify(task));

        Task
          .findById(task._id)
          .exec(function (err, tasks) {
              console.log("task found")
              tasks.title = task.title;
              tasks.description = task.description;
              tasks.modified_date = new Date().getDate();

              tasks.save(function (err) {
                  console.log("task updated")
                  console.log(JSON.stringify(err));
                  res.status(200).json(tasks);
              });
          });

    }

};
