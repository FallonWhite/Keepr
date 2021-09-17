<template>
  <div class="modal"
       :id="'keep-modal-'+keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="KeepModalTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header bg-dark py-1 justify-content-around">
          <h6 class="d-flex align-items-center m-1">
            <span><i class="far fa-eye"></i></span>{{ keep.views }}
          </h6>
          <h6 class="d-flex align-items-center m-1">
            <span><i class="fas fa-lock"></i></span>{{ keep.keeps }}
          </h6>
          <h6 class="d-flex align-items-center m-1">
            <span><i class="fas fa-share-alt"></i></span>{{ keep.views }}
          </h6>
          <!-- <h3 class="modal-title">
            {{ keep.name }}
          </h3> -->
          <!-- <button type="button" class="close text-light" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button> -->
        </div>
        <div class="modal-body bg-dark">
          <div class="container-fluid">
            <div class="row" v-if="keep.img">
              <!-- <div class="row" v-if="state.user && state.myVaults"> -->
              <div class="d-flex col-6 p-1">
                <!-- <img class="w-100 h-100 cover shadow-light" modal :src="keep.img" alt="Keep Image"> -->
                <img class="w-100 h-100 cover shadow-light" :src="keep.img">
              </div>
              <h3 class="modal-title text-align-center p-2">
                {{ keep.name }}
              </h3>
              <p class="text-left">
                {{ keep.description }}
              </p>
            </div>
            <div class="border-top p-1"></div>
          </div>
        </div>
        <div class="modal-footer bg-dark">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style lang="scss">
.modal-backdrop.show{
  opacity: 1!important;
  background:  var(--fade);
  backdrop-filter: blur(30px) brightness(.5) contrast(.85);
}
.modal-body{
  height: 50vh;
  overflow-y: auto;
}
.cover {
  object-fit: cover;
}
</style>
