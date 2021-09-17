<template>
  <div class="home flex-grow-1 container">
    <div class="row d-flex">
      <!-- <div v-if="loading" class="col text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div> -->
      <div class="card-columns col-md-12 mt-4">
        <Keep v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
// import { vaultsService } from '../services/VaultsService'

export default {
  // name: 'Home'
  setup() {
    const loading = ref(true)
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
        // await vaultsService.getVaults()
        // const loading = false
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      loading,
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep)
    }
  }
}
</script>

<style scoped lang="scss">
// .home{
//   text-align: center;
//   user-select: none;
//   > img{
//     height: 200px;
//     width: 200px;
//   }
// }

</style>
