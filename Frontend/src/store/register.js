import send from '../helpers/send'

export default {
    state: {
        count: null,
    },
    mutations: {
        // count
        registerCount: (state, payload) => {
            state.count = payload.count
        },
    },
    actions: {
        registerCount: async (context) => {
            const data = await send.get(context, '/api/register/count')

            context.commit('registerCount', data)
        },
        // 
        registerSend: async (context, payload) => {
            try {
                await post(context, '/api/register/register', payload)

                context.commit('pageChange', 'register-done')
            }
            catch (err) {
                console.error(err);
                context.commit('pageChange', 'error')
            }
        },
    },
}
