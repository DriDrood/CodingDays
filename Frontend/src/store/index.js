import { createStore } from 'vuex'
import hint from './hint'
import notifications from './notifications'

export default createStore({
  state: {
    registerCount: null,
  },
  mutations: {
    // count
    getRegisterCount: (state, payload) => {
      state.registerCount = payload.count
    },
  },
  actions: {
    getRegisterCount: async (context) => {
      const data = await get(context, '/api/register/count')

      context.commit('getRegisterCount', data)
    },
    // 
    sendRegister: async (context, payload) => {
      try
      {
        await post(context, '/api/register/register', payload)

        context.commit('pageChange', 'register-done')
      }
      catch (err)
      {
        console.error(err);
        context.commit('pageChange', 'error')
      }
    },
  },
  modules: {
    hint,
    notifications
  }
})
