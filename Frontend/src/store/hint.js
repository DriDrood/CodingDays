import encode from "../helpers/encode"
import send from '../helpers/send'

export default {
    state: {
        response: {
            text: null,
            imageUrl: null,
        }
    },
    mutations: {
        // type, message
        hintSendCode: (state, payload) => {
            state.response = payload;
        },
    },
    actions: {
        // code
        hintSendCode: async (context, payload) => {
            const encodedCode = encode.encode(payload.code)
    
            const data = await send.post(context, '/api/hint/try', { cypherResult: encodedCode, teamId: '15c1b27b-db31-42ac-b935-5d8b1e040e1a' })

            context.commit('hintSendCode', data)
            
            const notification = data.alreadyUsed
                ? { text: 'Toto řešení už bylo použito', type: 'warning' }
                : { text: 'Správné řešení', type: 'success' }
            context.dispatch('notificationsAdd', notification)
        },
    }
}