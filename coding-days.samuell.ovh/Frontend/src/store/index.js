import { createStore } from 'vuex'
import hint from './hint'
import home from './home'
import login from './login'
import notifications from './notifications'
import register from './register'

export default createStore({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    hint,
    home,
    login,
    notifications,
    register,
  }
})
