import Vue from 'vue'
import App from './App.vue'
import router from './router'
import bootstrapJs from 'bootstrap/dist/js/bootstrap.bundle'
import BootstrapCss from 'bootstrap/dist/css/bootstrap.min.css'
import bootstrapFont from '../node_modules/bootstrap-icons/font/bootstrap-icons.css'
import Axios from 'axios'
import Vuevalidate from 'vuelidate'


Axios.defaults.withCredentials = true;

Vue.prototype.$http = Axios;
Vue.config.productionTip = false
Vue.use(BootstrapCss);
Vue.use(bootstrapJs);
Vue.use(bootstrapFont);
Vue.use(Vuevalidate);




new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
