const express = require('express');
const app = express();
const bodyParser = require('body-parser');
const mysql = require('mysql');

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: true
}));


// connection config
const mc = mysql.createConnection({
    host: 'sql7.freemysqlhosting.net',
    user: 'sql7236763',
    password: 'uCb83jQSky',
    database: 'sql7236763'

});

// connect to db
mc.connect();

// default route
app.get('/', function (req, res) {
    return res.send({ error: true, message: 'hello' })
});

// port must be set to 8080 because incoming http requests are routed from port 80 to port 8080
app.listen(1337, function () {
    console.log('Node app is running on port 1337');
});


app.post('/api/login', function(req, res) {

    let json = (req.body);

    let email = json.email;
    let password = json.password;

    mc.query('SELECT * FROM users WHERE email = ?', [email], function (error, results, fields) {
        if (error) throw error;
        else {
            if (results.length > 0) {

                if (results[0].pass === password) {
                    console.log("login sucessful");
                    return res.send(results[0].id.toString())
                }
            }
        }
        console.log("login failed");
        return res.sendStatus(404);

    });
});

// get all partims of a student

app.get('/api/partims/student/:student_id', function(req, res){
    let studentId = req.params.student_id;
    console.log(studentId);
    mc.query(`select p.*, u2.naam as teacher_naam from users u
        join partim_students ps on ps.student_id = u.id 
        join partims p on p.id = ps.partim_id
        join users u2 on p.teacher_id = u2.id
        where u.id = ?`, [parseInt(studentId)] , function(error, results, fields){
            if (error) throw error;
            else if (results.length >= 1){
                return res.send(JSON.stringify(results));
            }
            else return res.sendStatus(404);

    })

});

// get partims by teacherID

app.get('/api/partims/teacher/:teacher_id', function(req, res){
    let teacherId = req.params.teacher_id;



    mc.query(`select p.*, u.naam as teacher_naam from partims p
            join users u on p.teacher_id = u.id
            where teacher_id = ?`, [parseInt(teacherId)] , function(error, results, fields){
        if (error) throw error;
        else{
            return res.send(JSON.stringify(results))
        }

    })

});

// get questions by partim

app.get('/api/questions/:partim_id', function(req, res){
    let partimId = req.params.partim_id;

    mc.query('select * from questions where partim_id = ?', [partimId], function(error, results, fields){
        if (error) throw error;
        else {return res.send(JSON.stringify(results))}


        })
    });


// add question to partim

app.post('/api/questions/add', function(req, res){
   let json = req.body;

   console.log(json);
   let userId = json.TheUser.Id;
   let question = json.TheQuestion;
   let partimId = json.TheClass.Id;


    mc.query('insert into questions (vraag, user_id, partim_id) VALUES (?, ?, ?)',
      [question, userId, partimId], function(error, results, fields){
            if (error) throw error;
            else{
                res.sendStatus(200);
            }
       })

});

// New partim

app.post('/api/partims/add', function(req, res){
   let json = req.body;
    console.log(json);
   let teacher_id = json.Id;
   let partimNaam = json.PartimName;
   let partimCode = json.PartimCode;

   mc.query('insert into partims (teacher_id, naam, code) VALUES (?, ?, ?)',
       [teacher_id, partimNaam, partimCode], function(error, results, fields){
            if(error) throw error;
            else {
                res.sendStatus(200);
            }
       })



});

// Register to partim

app.post('/api/partims/register', function(req, res){
    let json = req.body;

    let studentId = json.Id;
    let classCode = json.Code;

    let partimToSubscribe;

    mc.query('select * from partims where code = ?', [classCode], function(error, results, fields){
        if (error) throw error;
        if (results.length < 1){
            return res.sendStatus(404)
        }
        else {
            console.log(results);
            partimToSubscribe = results;
            mc.query('insert into partim_students (student_id, partim_id) VALUES (?, ?)',
                [studentId, partimToSubscribe[0].id], function(error, results, fields){
                    if (error) throw error;
                    else {
                        return res.sendStatus(200);
                    }

                })
        }

    });


});








