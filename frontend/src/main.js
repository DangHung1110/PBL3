import { createApp } from 'vue'
import App from './App.vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faUser } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import router  from './router';
library.add(faUser)
createApp(App)
.use(router)
.mount('#app')
app.component('font-awesome-icon', FontAwesomeIcon)

app.mount('#app')