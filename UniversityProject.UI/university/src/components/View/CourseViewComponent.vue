<template>

 <div class="container">
        <h3 class="mt-5">Course List</h3>
        <table class="table table-hover">
        <thead>
            <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="course in listCourse" :key="course.id" v-on:click="GetById(course.id)" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <th scope="row">{{course.id}}</th>               
                <td>{{course.name}}</td>
            </tr>
        </tbody>
    </table>

        <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Course Data</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                 <div class="col-6">
                    <b>Id</b>
                    <input type="number" class="form-control text-center" disabled id="" placeholder="Id" v-model="id">
                </div>
                <div class="col-6">
                    <b>Name</b>
                    <input type="text" class="form-control" id="" placeholder="Id" v-model="name">
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
    name:"CourseViewComponent",
    props:{
        msg:String
    },
    data: function(){
        return {
           id:'',
           name:'',
           listCourse: null
        }   
    },
    mounted:function(){
       this.$http.get("https://localhost:44307/api/Course?").then(data => {
        console.log(data);
        this.listCourse = data.data.data;
       })
    },
    methods:{
        GetById(id){
            this.$http.get(`https://localhost:44307/api/Course/${id}`).then(data => {
                this.name= data.data.data.name
                this.id = data.data.data.id
            })           
        },
        UpdateById(id){
             let json = {
                    "id": this.id,
                    "name": this.name
                };
            this.$http.put(`https://localhost:44307/api/Course/${id}`,json).then(data => {
                console.log(data)  
                this.$http.get("https://localhost:44307/api/Course?").then(data => {this.listCourse = data.data.data;})             
            })
        },
        DeleteById(id){
            this.$http.delete(`https://localhost:44307/api/Course/${id}`).then(data => {
                console.log(data)
                this.$http.get("https://localhost:44307/api/Course?").then(data => {this.listCourse = data.data.data;}) 
            })
        }
    }
}
</script>

<style>
#close{
    margin-right: 185px;;
}
</style>