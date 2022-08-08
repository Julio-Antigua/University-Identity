<template>

  <div id="app" class="">
    <div>
      <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
      <img  src="https://upload.wikimedia.org/wikipedia/commons/5/57/Instituto_Tecnol%C3%B3gico_de_Santo_Domingo_INTEC_LOGO.png" id="icon" alt="User Icon" style="width: 100px;">
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="btn btn-outline-danger" to="/dashboard">Home</router-link>
          </li>
          <li class="nav-item dropdown">
            <a class="btn btn-outline-danger dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              Dropdown Add
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li><a class="dropdown-item" v-on:click="StudentAdd()">Student</a></li>
              <li><a class="dropdown-item" v-on:click="SubjectAdd()">Subject</a></li>
              <li><a class="dropdown-item" v-on:click="CourseAdd()">Course</a></li>
            </ul>
          </li>
          <li class="nav-item dropdown">
            <a class="btn btn-outline-danger dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              Dropdown View
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li><a class="dropdown-item" v-on:click="StudentView()">Student</a></li>
              <li><a class="dropdown-item" v-on:click="SubjectView()">Subject</a></li>
              <li><a class="dropdown-item" v-on:click="CourseView()">Course</a></li>
              <li><hr class="dropdown-divider"></li>
              <li><a class="dropdown-item" v-on:click="DetailsSubjectView()">Details Subject</a></li>
            </ul>
          </li>
        </ul>
        <form class="d-flex">
          <button class="btn btn-danger bi bi-person-circle mx-2">{{user}}</button>         
          <button class="btn btn-outline-dark" type="submit" v-on:click="Logout()">Logout</button>
        </form>
      </div>
    </div>
  </nav>
    </div>
    <div class="" >
       <router-view/>
    </div>
   <footer>
      <div class="container-footer">
        <div class="content-foo">
          <h6>Phone</h6>
          <p>829-631-0278</p>
        </div>
        <div class="content-foo">
          <h6>Email</h6>
          <p>informacion@intec.edu.do</p>
        </div>
        <div class="content-foo">
          <h6>Location</h6>
          <p>Av. de los Próceres 49, Santo Domingo 10602</p>
        </div>
      </div>
      <h2 class="title-end">&copy;INTEC</h2>
   </footer>
  </div>
  
  
</template>

<script>
export default{
    name: 'App',
    props:{
        msg:String
    },
    components:{
    },
    data:function(){
      return{
        user: localStorage.getItem('user')
      }
    },mounted(){
      this.user = localStorage.getItem('user')
    },
    methods:{
      Logout(){
        this.$http.post("https://localhost:44307/api/Auth/logout");
        localStorage.removeItem("user");
        this.$router.push('/login')
      },

      StudentAdd(){
        this.$router.push({name:'studentAdd'})
      },
      StudentView(){
        this.$router.push({name:'studentView'})
      },

      SubjectView(){
        this.$router.push({name:'subjectview'})
      },
      SubjectAdd(){
        this.$router.push({name:'subjectAdd'})
      },

      CourseView(){
        this.$router.push({name:'courseView'})
      },
      CourseAdd()
      {
        this.$router.push({name:'courseAdd'})
      },

      DetailsSubjectView(){
        this.$router.push({name:'detailsSubjectView'})
      }
    }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #f00404c9;
}

  
    a{
         margin: 6px;
    }
    a.dropdown-item{

       color: #f00404c9;
    }
     a.dropdown-item:hover{
      background-color: #f00404c9;
      color: #fff
    }

   
   /* footer */

   footer{
    background: #414141;
    padding: 20px 0 10px 0;
    margin: auto;
    overflow: hidden;
    color: #fff;
   }
   
   .container-footer{
  display: flex;
  width: 90%;
  justify-content: space-evenly;
  margin: auto;
  padding-bottom: 10px;
  border-bottom: 1px solid #ccc;
  }

  .container-foo{
    text-align: center;
  }

  .content-foo h6{
    color: #fff;
    border-bottom: 3px solid #ff3232;
    padding-bottom: 5px;
    margin-bottom: 10px;
  }

 .content-foo p{
    color: #ccc;

  }


  .title-end{
    text-align: center;
    font-size: 24px;
    margin: 20px 0 0 0;
    color: #979797;
  }

</style>
