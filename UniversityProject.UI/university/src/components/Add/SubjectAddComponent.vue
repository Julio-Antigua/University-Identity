<template>

    <div class="container">
        <div class="wrapper">
            <div id="formContent">
                
                <h4 class="text text-danger">Create Subject</h4>

                <form v-on:submit.prevent="AddSubject" >   

                <div class="row" id="form">
                    <div class="col-8 mb-4">
                        <b class="text-left">Subject Name</b>
                        <input type="text" id="name"  class="form-control" :class="{'is-invalid':validationStatus($v.name)}"  name="name" placeholder="Name" v-model.trim="$v.name.$model" >
                        <div v-if="!$v.name.$required" class="invalid-feedback text-danger">The Name field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b class="text-left">Name Course</b>
                        <select class="form-select form-select" id="courses" aria-label=".form-select-sm example" :class="{'is-invalid':validationStatus($v.courses)}" name="courses" placeholder="courses" v-model="courses">
                            <option v-for="course in listCourse" :key="course.id" :value="course.id"  >{{course.name}}</option>
                        </select>
                        <div v-if="!$v.courses.$required" class="invalid-feedback text-danger">The Course field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b class="text-left">Start Time</b>
                        <input type="date" id="starttime" class="form-control" :class="{'is-invalid':validationStatus($v.starttime)}" name="starttime" placeholder="starttime" v-model.trim="$v.starttime.$model">
                        <div v-if="!$v.starttime.$required" class="invalid-feedback text-danger">The StartTime field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b class="text-left">End Time</b>
                        <input type="date" id="endtime"  class="form-control" :class="{'is-invalid':validationStatus($v.endtime)}"  name="endtime" placeholder="endtime" v-model.trim="$v.endtime.$model" >
                        <div v-if="!$v.endtime.$required" class="invalid-feedback text-danger">The endtime field is required</div>
                    </div>
    
                    <div class="col-5 mt-5" id="button">
                            <input type="submit" class="btn btn-primary mx-2" id="signin" value="Create">
                            <input type="button" class="btn btn-danger" id="signin" value="Home" v-on:click="BackToHome()">                         
                    </div>
                </div> 
 
                </form>

            </div>
        </div>
    </div>

</template>

<script>
import {required} from 'vuelidate/lib/validators'
export default{
    name: 'subjectAddComponent',
    props: {
        msg: String
    },
    data: function(){
        return {
            id:'',
            name: '',
            starttime: '',
            endtime:'',
            courses: '',
            listCourse:null
        }
    },
    validations:{
        name: {required},
        starttime:{required},
        endtime: {required},
        courses: {required}
        
    },
    mounted:function(){
        this.$http.get('https://localhost:44307/api/Course/').then(data => {
                console.log(data.data.data)
                this.listCourse = data.data.data
            })
    },
    methods:{
        validationStatus:function(validation){
            return typeof validation != "undefined" ? validation.$error : false;
        },
        AddSubject(){
            this.$v.$touch();
             if(this.$v.$pendding || this.$v.$error)
             {
                return;
             } 
                let json = {
                    'name' : this.name,
                    'idCourse' : this.courses,
                    'starttime': this.starttime, 
                    'endtime': this.endtime,
                }
                this.$http.post('https://localhost:44307/api/Subject/',json).then(data => {
                        console.log(data.data.data)
                        this.$router.push('/dashboard')                   
                })         
        },
        BackToHome(){
            this.$router.push('/dashboard')
        }
    }
}
</script>

<style>
h4{
    margin-right: 40px;
    font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
    font-size: 40px;
    padding-top: 15px;
}

    img{
        width: 65px;
    }
    #form{
        display: flex;
        flex-direction:column;
        margin-left: 120px;
    }
     #button{
        display: flex;
        justify-content: space-around;
        margin-left: 70px;
        margin-bottom: 20px;
    }
    #signin{
        padding-left: 50px;
        padding-right: 50px;
    } 
    #subjectList{
         padding-left: 70px;
        padding-right: 70px;
    }
    .wrapper {
    display: flex;
    align-items: center;
    flex-direction: column; 
    justify-content: center;
    width: 100%;
    min-height: 100%;
    padding: 20px;
    }

#formContent {
    
  -webkit-border-radius: 10px 10px 10px 10px;
  border-radius: 10px 10px 10px 10px;
  background: #fff;
  padding: 30px;
  width: 90%;
  max-width: 650px;
  position: relative;
  padding: 0px;
  -webkit-box-shadow: 0 30px 60px 0 rgba(0,0,0,0.3);
  box-shadow: 0 30px 60px 0 rgba(0,0,0,0.3);
  text-align: center;
}
.text-left{
    margin-right: 230px;
}
</style>