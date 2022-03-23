import { createStore } from 'vuex'
import send from './send'
import notifications from './notifications'

export default createStore({
  state: {
    token: null,
    dashboard: null,
  },
  mutations: {
    // token
    login: (state, payload) => {
      state.token = payload.token
    },
    // dashboard
    loadDashboard: (state, payload) => {
      state.dashboard = payload.dashboard
    },
  },
  actions: {
    // name, password
    login: async (context, payload) => {
      const data = await send.post(context, '/api/session/login', payload)
      context.commit('login', data)
    },
    //
    loadDashboard: async (context) => {
      const data = await send.get(context, '/api/private/getDashboard')
      context.commit('loadDashboard', data)
    },
  },
  modules: {
    notifications,
  },
})
