var mongoose = require('mongoose');

var taskSchema = new mongoose.Schema({
    userid: {
        type: String,
        required: true
    },
    title: {
        type: String,
        required: true
    },
    description: {
        type: String,
        required: true
    },
    created_date: {
        type: Date,
        required: true
    },
    modified_date: {
        type: Date,
        required: false
    }
});


mongoose.model('Task', taskSchema);
