import Vue from 'vue'
import VueRouter from 'vue-router'
import LoginView from '../views/View/LoginView.vue'




Vue.use(VueRouter)

// export const url = 'https://localhost:44307/api/';
// const urlStudent = 'Student/';
// const urlSubject = 'Subject/';
// const urlCourse = 'Course/';
// const urlAuthLogin = 'Auth/login/';
// const urlAuthLogout = 'Auth/logout/';
// const urlAuthSignin = 'Auth/signin/';
// const urlAuthUpdateRole = 'Auth/updateRoleByUser/';

const routes = [
  {
    path: '/login',
    name: 'home',
    component: LoginView
  },
  {
    path: '/signin',
    name: 'signin',
    component: () => import('../views/View/SigninView.vue')
  },
  {
    path: '/dashboard',
    name:'dashboard',
    component: () => import('../views/View/DashboardView.vue')
  },

  {
    path: '/dashboard/student',
    name: 'studentAdd',
    component: () => import('../views/Add/StudentAddView.vue')
  },
  {
    path: '/dashboard/view/student',
    name: 'studentView',
    component: () => import('../views/View/StudentView.vue')
  },

  {
    path: '/dashboard/add/subject',
    name: 'subjectAdd',
    component: () => import('../views/Add/SubjectAddView.vue')
  },
  {
    path: '/dashboard/view/subject',
    name: 'subjectview',
    component: () => import('../views/View/SubjectView.vue')
  },


  {
    path: '/dashboard/add/course',
    name : 'courseAdd',
    component: () => import('../views/Add/CourseAddView.vue')
  },
  {
    path: '/dashboard/view/course',
    name : 'courseView',
    component: () => import('../views/View/CourseView.vue')
  },

  {
    path: '/dashboard/view/detailssubject',
    name: 'detailsSubjectView',
    component: () => import('../views/View/DetailsSubjectView.vue')
  },
 
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
