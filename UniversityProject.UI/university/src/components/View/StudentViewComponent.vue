<template>

    <div class="container">
         <h3 class="mt-5">Student List</h3>
        <form id="filter" v-on:submit.prevent="filter()">
            <div class="container" id="filter">
             <div class="row m-5">
                 
                <div class="col-3">
                    <b>Id Student</b>
                    <input type="number" class="form-control" id="" placeholder="Id" v-model="id">
                </div>
                <div class="col-3">
                    <b>FirstName</b>
                    <input type="text" class="form-control" id="" placeholder="FirstName" v-model="firstname">
                </div>
                <div class="col-3">
                    <b>Entry Date</b>
                    <input type="datetime-local" class="form-control" id="" placeholder="EntryDate" v-model="entrydate">
                </div>
                <div class="col-2">
                    <button class="btn btn-danger mt-4 form-control"><b>F i l t e r</b></button>
                </div>
            </div>     
        </div>
        </form>        
       
        <table class="table table-hover">
        <thead>
            <tr>
            <th scope="col">Id</th>
            <th scope="col">FirstName</th>
            <th scope="col">LastName</th>
            <th scope="col">DateOfBirth</th>
            <th scope="col">Email</th>
            <th scope="col">EntryDate</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="student in listUser" :key="student.id" v-on:click="GetById(student.id)" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <th scope="row">{{student.id}}</th>               
                <td>{{student.firstName}}</td>
                <td>{{student.lastName}}</td>
                <td>{{student.dateOfBirth}}</td>
                <td>{{student.email}}</td>
                <td>{{student.entryDate}}</td>
            </tr>
        </tbody>
    </table>

    <nav aria-label="Page navigation example">
        <ul class="pagination">
            <li class="page-item">
            <a class="page-link" href="#" aria-label="Previous" v-on:click="PreviousPage()">
                <span aria-hidden="true">&laquo;</span>
            </a>
            </li>
            <li class="page-item" v-for="p in totalPage" :key="p" :class="{'active':(p == currentPage)}"><a class="page-link" href="#">{{p}}</a></li>
            <li class="page-item">
            <a class="page-link" href="#" aria-label="Next" v-on:click="NextPage()">
                <span aria-hidden="true">&raquo;</span>
            </a>
            </li>
        </ul>
    </nav>

        <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Student Data</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                 <div class="col-12">
                    <b>Id Student</b>
                    <input type="number" class="form-control text-center" disabled id="" placeholder="Id" v-model="idStudent">
                </div>
            </div> 
             <div class="row">
                <div class="col-6">
                    <b>LastName</b>
                    <input type="text" class="form-control" id="" placeholder="Id" v-model="lastnameStudent">
                </div>
                <div class="col-6">
                    <b>FirstName</b>
                    <input type="text" class="form-control" id="" placeholder="FirstName" v-model="firstnameStudent">
                </div>
                <div class="col-6">
                    <b>Email</b>
                    <input type="text" class="form-control" id="" placeholder="FirstName" v-model="emailStudent">
                </div>
                <div class="col-6">
                    <b>Date Of Birth</b>
                    <input type="datetime-local"  class="form-control" id="" placeholder="EntryDate" v-model="dateOfBirthStudent">
                </div> 
            </div>       
        </div>
        <div class="modal-footer">
            <button type="button" id="close" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-danger" v-on:click="DeleteById(idStudent)" data-bs-dismiss="modal">Delete</button>
            <button type="button" class="btn btn-primary" v-on:click="UpdateById(idStudent)" data-bs-dismiss="modal">Save changes</button>
        </div>
        </div>
    </div>
    </div>

    </div>
    

</template>

<script>
export default{
    name:"StudentViewComponent",
    props:{
        msg:String
    },
    data: function(){
        return {
           id:'',
           firstname:'',
           entrydate:'', 
           clean:'',

           listUser: null,

           lastnameStudent: '',
           firstnameStudent: '',
           idStudent: '',
           emailStudent:'',
           dateOfBirthStudent: '',

           totalPage: 0,
           currentPage:1,
           nextPageUrl:'',
           previousPageUrl: ''
        }   
    },
    mounted:function(){
       this.$http.get("https://localhost:44307/api/Student?").then(data => {
        console.log(data);
        this.listUser = data.data.data;
        this.totalPage = data.data.meta.totalPage
        this.nextPageUrl = data.data.meta.nextPageUrl
        this.previousPageUrl = data.data.meta.previousPageUrl
       })
    },
    methods:{
        PreviousPage(){
             this.$http.get(`${this.previousPageUrl}`).then(data => {
                this.listUser = data.data.data;
                this.totalPage = data.data.meta.totalPage
                this.currentPage = data.data.meta.currentPage
                this.nextPageUrl = data.data.meta.nextPageUrl
                this.previousPageUrl = data.data.meta.previousPageUrl
            })
        },
        NextPage(){
            this.$http.get(`${this.nextPageUrl}`).then(data => {
                this.listUser = data.data.data;
                this.totalPage = data.data.meta.totalPage
                this.currentPage = data.data.meta.currentPage
                this.nextPageUrl = data.data.meta.nextPageUrl
                this.previousPageUrl = data.data.meta.previousPageUrl
            })
        },

        filter(){
              let json = {
                    "id" : this.id,
                    "firstName":  this.firstname,
                    "entryDate": this.entrydate
                };
        
            this.$http.get(`https://localhost:44307/api/Student?IdStudent=${json.id}&FirstName=${json.firstName}&EntryDate=${json.entryDate}`).then(data => {
                console.log(data);
                this.listUser = data.data.data;

                this.id = '',
                this.firstname = '',
                this.entryDate = '',
                this.clean = ''
            })
        },
        GetById(id){
            this.$http.get(`https://localhost:44307/api/Student/${id}`).then(data => {
                this.lastnameStudent= data.data.data.lastName
                this.firstnameStudent = data.data.data.firstName
                this.emailStudent = data.data.data.email
                this.dateOfBirthStudent = data.data.data.dateOfBirth
                this.idStudent = data.data.data.id
                console.log(data)
            })           
        },
        // AddStudent(){
        //     axios.post().then(data => {
                
        //     })
        // },
        UpdateById(id){
             let json = {
                    "id" : this.idStudent,
                    "firstName":  this.firstnameStudent,
                    "lastName": this.lastnameStudent,
                    "email": this.emailStudent,
                    "dateOfBirth": this.dateOfBirthStudent
                };
            this.$http.put(`https://localhost:44307/api/Student/${id}`,json).then(data => {
                console.log(data)  
                this.$http.get("https://localhost:44307/api/Student?").then(data => {this.listUser = data.data.data;})             
            })
        },
        DeleteById(id){
            this.$http.delete(`https://localhost:44307/api/Student/${id}`).then(data => {
                console.log(data)
                this.$http.get("https://localhost:44307/api/Student?").then(data => {this.listUser = data.data.data;}) 
            })
        }
    }
}
</script>

<style>
#filter{
    margin-left: 50px;
}
#close{
    margin-right: 185px;;
}
</style>