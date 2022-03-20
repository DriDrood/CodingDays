import { createStore } from 'vuex'
import encode from "./encode";

const get = async (context, url) => {
  try
  {
    context.commit('setLoading', true)

    const result = await fetch(url)
    return await result.json()
  }
  finally
  {
    context.commit('setLoading', false)
  }
}
const post = async (context, url, data) => {
  try
  {
    context.commit('setLoading', true)

    const result = await fetch(url, { method: 'POST', headers: { 'Content-type': 'application/json'}, body: JSON.stringify(data) })
    return await result.json()
  }
  finally
  {
    context.commit('setLoading', false)
  }
}

export default createStore({
  state: {
    page: 'register',
    loading: false,
    registerCount: null,
    help: {
      type: null,
      message: null,
    },
  },
  mutations: {
    // page
    pageChange: (state, payload) => {
      state.page = payload.page
    },
    setLoading: (state, payload) => {
      state.loading = payload
    },
    // count
    getRegisterCount: (state, payload) => {
      state.registerCount = payload.count
    },
    sendRegister: (state, ) => {

    },
    // type, message
    sentHelpCode: (state, payload) => {
      state.help = payload;
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
    // code
    sentHelpCode: async (context, payload) => {
      const encodedCode = encode.encode(payload.code)

      const data = await post(context, '/api/help/try', { code: encodedCode })

      context.commit('sentHelpCode', data)
    },
  },
  modules: {
  }
})
