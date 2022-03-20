import { createRouter, createWebHistory } from 'vue-router'
import Register from '../views/register.vue'
import Help from '../views/help.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Register,
    // component: () => import('../views/register.vue'),
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
  {
    path: '/help',
    name: "Help",
    component: Help,
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
