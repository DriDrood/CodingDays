import encode from "../helpers/encode"
import { get, post } from '../helpers/send'

export default {
    state: {
        response: {
            text: null,
            image: null,
        }
    },
    mutations: {
        // type, message
        sentHelpCode: (state, payload) => {
            state.help = payload;
        },
    },
    actions: {
        // code
        helpSendCode: async (context, payload) => {
            const encodedCode = encode.encode(payload.code)
    
            const data = await post(context, '/api/help/try', { code: encodedCode })
    
            context.commit('sentHelpCode', data)
        },
    }
}