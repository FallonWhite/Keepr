<template>
  <div class="home flex-grow-1 container">
    <div class="row d flex">
      <div class="col-md-12">
        <ActiveKeep v-if="activeKeep" />
      </div>
    </div>
    <div class="row">
      <div v-if="loading" class="col text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div v-else class="masonry">
        <Keep v-for="k in keeps" :key="k.id" :keep="k" class="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
// import { keepsService } from '../services/KeepsService'
// import { vaultsService } from '../services/VaultsService'

export default {
  // name: 'Home'
  setup() {
    const loading = ref(true)
    onMounted(() => {
      try {
      // await keepsService.getAllKeeps()
      // await vaultsService.getVaults()
        loading.value = false
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
.masonry {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  max-height: 1000;
  margin: 2;
  .keep {
    width: 150px;
    background-size: cover;
    background-color: maroon;
    color: black;
    margin: 0 1rem 1rem 0;
  }
}
</style>
