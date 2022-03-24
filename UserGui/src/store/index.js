import { createStore } from 'vuex'
import send from './send'
import notifications from './notifications'

export default createStore({
  state: {
    token: null,
    resetPasswordToken: null,
    dashboard: null,
  },
  mutations: {
    // token
    login: (state, payload) => {
      state.token = payload.token
    },
    // token
    forgot: (state, payload) => {
      state.resetPasswordToken = payload.token
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
    // name
    forgot: async (context, payload) => {
      await send.post(context, '/api/session/forgotPassword', payload)
      
      context.dispatch('notificationsAdd', { text: 'Byl vám odeslán e-mail', type: 'success' })
    },
    // name, newPassword
    resetPassword: async (context, payload) => {
      const data = { ...payload, token: context.state.resetPasswordToken }
      await send.post(context, '/api/session/resetPassword', data)
      
      context.commit('forgot', { token: null })
      context.dispatch('notificationsAdd', { text: 'Heslo bylo úspěšně změněno', type: 'success' })
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
