<template>

    <div class="container" >
        <form id="filter" >
            <div class="container" id="filter">
             <div class="row m-5">
                <div class="col-3">
                    <b>Name Subject</b>
                    <select class="form-select form-select" aria-label=".form-select-sm example" v-model="subjectlist" v-on:click="filterSubject()">
                        <option value="0">Default</option>
                        <option v-for="subject in listSubject" :key="subject.id" :value="subject.id"  >{{subject.name}}</option>
                    </select>
                </div>
                <div class="col-3">
                    <b>Name Student</b>
                    <select class="form-select form-select" aria-label=".form-select-sm example" v-model="studentlist" v-on:click="filterStudent()">
                        <option value="0">Default</option>
                        <option v-for="student in listStudent" :key="student.id" :value="student.id" >{{student.firstName}}</option>
                    </select>
                </div>
            </div>     
        </div>
        </form>        
       
        <table class="table table-hover">
        <thead>
            <tr>
            <th scope="col">IdStudent</th>
            <th scope="col">FirstName</th>
            <th scope="col">LastName</th>
            <th scope="col">Email</th>
            <th scope="col">Subject</th>
            <th scope="col">IdSubject</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="details in listDetails" :key="details.idStudent" v-on:click="GetById(details.idStudent)" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <th scope="row">{{details.idStudent}}</th>               
                <td>{{details.firstName}}</td>
                <td>{{details.lastName}}</td>
                <td>{{details.email}}</td>
                <td>{{details.subject}}</td>
                <td>{{details.idSubject}}</td>
            </tr>
        </tbody>
    </table>

        <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Details subject Data</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
             <div class="row">
                <div class="col-6">
                    <b>LastName</b>
                    <input type="text" class="form-control" id="" placeholder="LastName" v-model="lastnameStudent" disabled>
                </div>
                <div class="col-6">
                    <b>FirstName</b>
                    <input type="text" class="form-control" id="" placeholder="FirstName" v-model="firstnameStudent" disabled>
                </div> 
            </div>       
        </div>
        <div class="modal-footer">
            <button type="button" id="close" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-danger" v-on:click="DeleteById(idStudent)" data-bs-dismiss="modal">Delete Student</button>
        </div>
        </div>
    </div>
    </div>

    </div>
    

</template>

<script>
export default{
    name:"DashboardComponent",
    props:{
        msg:String
    },
    data: function(){
        return {
           id:'',
           firstnameStudent: '',
           lastnameStudent: '',
           listDetails: null,
           listSubject: null,
           listStudent: null,
           subjectlist:0,
           studentlist:0
        }   
    },
    mounted:function(){
       this.$http.get("https://localhost:44307/api/Subject/").then(data => {
        console.log(data);
        this.listSubject = data.data.data;
       }),
        this.$http.get("https://localhost:44307/api/Student/").then(data => {
        console.log(data);
        this.listStudent = data.data.data;
       })
    },
    methods:{
        filterSubject(){  
            let json = {
                    "id" : this.subjectlist,
                };     
            this.$http.get(`https://localhost:44307/api/Student/Subject/${json.id}`).then(data => {
                console.log(data);
                this.listDetails = data.data.data;
            })
        },
        filterStudent(){  
            let json = {
                    "id" : this.studentlist,
                };     
            this.$http.get(`https://localhost:44307/api/Subject/Student/${json.id}`).then(data => {
                console.log(data);
                this.listDetails = data.data.data;
            })
        },
        GetById(id){
            this.$http.get(`https://localhost:44307/api/Student/${id}`).then(data => {
                this.lastnameStudent= data.data.data.lastName
                this.firstnameStudent = data.data.data.firstName
            })           
        },
        DeleteById(id){
            this.$http.delete(`https://localhost:44307/api/Student/Subject/${id}`).then(data => {
                console.log(data)
                this.$http.get("https://localhost:44307/api/Subject/").then(data => {this.listUser = data.data.data;}) 
            })
        }
    }
}
</script>

<style>
#filter{
    margin-left: 50px;
}

</style>