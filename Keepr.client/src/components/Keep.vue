<template>
  <div class="keep rounded bg-dark text-left selectable my-4 mx-1 p-0 pt-2 pl-2 pr-2" data-toggle="modal" :data-target="'#keep-modal-'+keep.id" title="Keep Details">
    <img class="card-img" :src="keep.img" alt="card image" style="cover">
    <div class="d-flex namePosition">
      <h5 class="text-light">
        {{ keep.name }}
      </h5>
      <!-- <router-link class="" :to="{name: 'ProfilePage', params: {id: keep.creatorId}}"> -->
      <img class="profimg justify-content: flex-end;" :src="keep.creator.picture" alt="profile image">
      <!-- </router-link> -->
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
  justify-content: flex-end;
  height: 3rem;
  border-radius: 50%;
}
.namePosition{
 left:2px;
 bottom: 10px;
}
</style>
