<template>
    
    <div class="container" id="subjectview">
       <h3 class="mt-5">Subject List</h3>
        <table class="table table-hover">
        <thead>
            <tr>
            <th scope="col">Id</th>
            <th scope="col">FirstName</th>
            <th scope="col">Id Course</th>
            <th scope="col">StartTime</th>
            <th scope="col">EndTime</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="subject in listSubject" :key="subject.id" v-on:click="GetById(subject.id)" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <th scope="row">{{subject.id}}</th>               
                <td>{{subject.name}}</td>
                <td>{{subject.idCourse}}</td>
                <td>{{subject.startTime}}</td>
                <td>{{subject.endTime}}</td>
            </tr>
        </tbody>
    </table>
    <!--  -->

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Subject Data</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                 <div class="col-12">
                    <b>Id</b>
                    <input type="number" class="form-control text-center" disabled id="" placeholder="Id" v-model="id">
                </div>
            </div> 
             <div class="row">
                <div class="col-6">
                    <b>Name</b>
                    <input type="text" class="form-control" id="" placeholder="Id" v-model="name">
                </div>
                <div class="col-6">
                    <b>Id Course</b>
                    <input type="text" class="form-control" id="" placeholder="FirstName" v-model="idCourse">
                </div>
                <div class="col-6">
                    <b>Start Time</b>
                    <input type="datetime-local" class="form-control" id="" placeholder="FirstName" v-model="startTime">
                </div>
                <div class="col-6">
                    <b>End Time</b>
                    <input type="datetime-local" class="form-control" id="" placeholder="EntryDate" v-model="endTime">
                </div> 
            </div>       
        </div>
        <div class="modal-footer">
            <button type="button" id="close" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-danger" v-on:click="DeleteById(id)" data-bs-dismiss="modal">Delete</button>
            <button type="button" class="btn btn-primary" v-on:click="UpdateById(id)" data-bs-dismiss="modal">Save changes</button>
        </div>
        </div>
    </div>
    </div>

    </div>
    

</template>

<script>
export default{
    name:"SubjectViewComponent",
    props:{
        msg:String
    },
    data: function(){
        return {
           id:'',
           name:'',
           idCourse:'', 
           startTime: '',
           endTime: '',
           listSubject: null
        }   
    },
    mounted:function(){
       this.$http.get("https://localhost:44307/api/Subject?").then(data => {
        console.log(data);
        this.listSubject = data.data.data;
       })
    },
    methods:{
        GetById(id){
            this.$http.get(`https://localhost:44307/api/Subject/${id}`).then(data => {
                this.name= data.data.data.name
                this.idCourse = data.data.data.idCourse
                this.startTime = data.data.data.startTime
                this.endTime = data.data.data.endTime
                this.id = data.data.data.id
                console.log(data)
            })           
        },
        UpdateById(id){
             let json = {
                    "id": this.id,
                    "name": this.name,
                    "idCourse": this.idCourse,
                    "startTime": this.startTime,
                    "endTime": this.endTime
                };
            this.$http.put(`https://localhost:44307/api/Subject/${id}`,json).then(data => {
                console.log(data)  
                this.$http.get("https://localhost:44307/api/Subject?").then(data => {this.listSubject = data.data.data;})             
            })
        },
        DeleteById(id){
            this.$http.delete(`https://localhost:44307/api/Subject/${id}`).then(data => {
                console.log(data)
                this.$http.get("https://localhost:44307/api/Subject?").then(data => {this.listSubject = data.data.data;}) 
            })
        }
    }
}
</script>

<style>
#close{
    margin-right: 185px;;
}
#subjectview{
    margin-bottom: 200px;
}
</style>