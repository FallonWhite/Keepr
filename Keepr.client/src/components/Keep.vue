<template>
  <div class="keep rounded bg-dark text-left selectable" data-toggle="modal" :data-target="'#keep-modal-'+keep.id" title="Keep Details">
    <img class="card-img-top" :src="keep.img" alt="card image" style="">
    <div class="d-flex">
      <h5> {{ keep.title }} </h5>
      <!-- <router-link class="" :to="{name: 'ProfilePage', params: {id: keep.creatorId}}">
        <img class="profimg" :src="keep.creator.picture" alt="profile image">
      </router-link> -->
    </div>
  </div>
  <KeepModal :keep="keep" />
</template>

<script>
import { computed, reactive } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
// import { profilesService } from '../services/ProfilesService'
// import Pop from '../utils/Notifier'
// import { useRouter, useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    // const router = useRouter()
    // const route = useRoute()
    const state = reactive({
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),

      async setActiveKeep() {
        keepsService.setActiveKeep(props.keep.id)
        keepsService.views(props.keep.id, props.keep)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.profimg {
  border-radius: 50%;
}
</style>
