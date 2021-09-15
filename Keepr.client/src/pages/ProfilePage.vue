<template>
  <div class="profile-page container-fluid">
    <div class="row m-3">
      <div class="col-md-2">
        {{ profile.picture }}
      </div>
      <div class="col-md-10">
        <h3 class="shadow">
          {{ profile.name }}
        </h3>
        <h5 class="shadow">
          Vaults: {{ vaults.length }}
        </h5>
        <h5 class="shadow">
          Keeps: {{ state.keeps.length }}
        </h5>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const router = useRoute()
    onMounted(async() => {
      try {
        await keepsService.getAll({ creatorId: router.params.id })
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
