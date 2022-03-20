import encode from "../helpers/encode"
import { get, post } from '../helpers/send'

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
            state.help = payload;
        },
    },
    actions: {
        // code
        hintSendCode: async (context, payload) => {
            const encodedCode = encode.encode(payload.code)
    
            const data = await post(context, '/api/hint/try', { code: encodedCode })
    
            context.commit('hintSendCode', data)
        },
    }
}