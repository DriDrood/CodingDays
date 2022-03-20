export default {
    get: async (context, url) => {
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
    },
    post: async (context, url, data) => {
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
}
