<template>

    <div class="container">
        <div class="wrapper">
            <div id="formContent">
                
                <div class="">
                
                </div>
                <h4 class="text text-danger">Create Student</h4>

                <form v-on:submit.prevent="AddStudent" >   

                <div class="row" id="form">
                    <div class="col-8 mb-4">
                        <b for="" class="text-left">FirstName</b>
                        <input type="text" id="firstname"  class="form-control" :class="{'is-invalid':validationStatus($v.firstname)}"  name="firstname" placeholder="Firstname" v-model.trim="$v.firstname.$model" >
                        <div v-if="!$v.firstname.$required" class="invalid-feedback text-danger">The FirstName field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b for="" class="text-left">LastName</b>
                        <input type="text" id="lastname" class="form-control" :class="{'is-invalid':validationStatus($v.lastname)}" name="lastname" placeholder="Lastname" v-model.trim="$v.lastname.$model">
                        <div v-if="!$v.lastname.$required" class="invalid-feedback text-danger">The Lastname field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b for="" class="text-left">DateOfBirth</b>
                        <input type="date" id="dateofbirth" class="form-control" :class="{'is-invalid':validationStatus($v.dateofbirth)}" name="dateofbirth" placeholder="DateOfBirth" v-model.trim="$v.dateofbirth.$model">
                        <div v-if="!$v.dateofbirth.$required" class="invalid-feedback text-danger">The DateOfBirth field is required</div>
                    </div>
                    <div class="col-8 mb-4">
                        <b for="" class="text-left">Email</b>
                        <input type="text" id="email"  class="form-control" :class="{'is-invalid':validationStatus($v.email)}"  name="email" placeholder="Email" v-model.trim="$v.email.$model" >
                        <div v-if="!$v.email.$required" class="invalid-feedback text-danger">The Email field is required</div>
                        <div v-if="!$v.email.$email" class="invalid-feedback text-danger">The Email is not valid</div>
                    </div>
                    <div class="col-8 ">
                        <div class="btn-group dropup">
                            <button class="btn btn-secondary dropdown-toggle" type="button" id="subjectList" data-bs-toggle="dropdown" aria-expanded="false">Subject List</button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" v-for="subject in listSubject" :key="subject.id">
                                <!-- Default unchecked -->
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" :id="subject.id" class="custom-control-input" :class="{'is-invalid':validationStatus($v.subjects)}" name="subject" placeholder="Subject" :value="subject.id" v-model="$v.subjects.$model">
                                    <label class="custom-control-label mx-2" :for="subject.id" :value="subject.id"><strong>{{subject.name}}</strong></label>
                                    <div v-if="!$v.subjects.$required" class="invalid-feedback text-danger">You must choose a subject</div>
                                </div>  
                                </a>
                            </div>
                        </div> 
                                   
                    </div>
                    <div class="col-5 mt-5" id="button">
                            <input type="submit" class="btn btn-primary mx-2" id="signin" value="Create">
                            <input type="button" class="btn btn-danger " id="signin" value="Home" v-on:click="BackToHome()">                
                    </div>
                </div> 
 
                </form>

            </div>
        </div>
    </div>

</template>

<script>
import {required,email} from 'vuelidate/lib/validators'
export default{
    name: 'studentComponent',
    props: {
        msg: String
    },
    data: function(){
        return {
            firstname: '',
            lastname:'',
            dateofbirth: '',
            email:'',
            subjects: [],
            listSubject:null
        }
    },
    validations:{
        firstname: {required},
        lastname: {required},
        dateofbirth:{required},
        email: {required,email},
        subjects: {required}
        
    },
    mounted:function(){
        this.$http.get('https://localhost:44307/api/Subject/').then(data => {
                console.log(data.data.data)
                this.listSubject = data.data.data
            })
    },
    methods:{
        validationStatus:function(validation){
            return typeof validation != "undefined" ? validation.$error : false;
        },
        AddStudent(){
            this.$v.$touch();
             if(this.$v.$pendding || this.$v.$error)
             {
                return;
             } 
                let json = {
                    'firstName' : this.firstname,
                    'lastName' : this.lastname,
                    'dateofBirth': this.dateofbirth, 
                    'email': this.email,
                    'subjectIds':this.subjects
                }
                this.$http.post('https://localhost:44307/api/Student/',json).then(data => {
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

<style scoped>
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
  margin-top: 5%;
  text-align: center;
  margin-bottom: 10%;
}
.text-left{
    margin-right: 300px;
}
</style>