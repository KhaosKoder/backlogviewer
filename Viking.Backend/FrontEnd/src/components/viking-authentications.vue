<template>
  <div class="global-image">
    <div class="rounded-box">
      <div class="date-inputs">
        <div class="date-input">
          <label>From</label>
          <dx-date-box v-model="from" :type="'datetime'"></dx-date-box>
        </div>
        <div class="date-input">
          <label>To</label>
          <dx-date-box v-model="to" :type="'datetime'"></dx-date-box>
        </div>
      </div>
      <button @click="search" :disabled="!isValid">Search</button>
    </div>
    <dx-data-grid :dataSource="gridData">
      <!-- Add column definitions and other grid settings here --> 
    </dx-data-grid>
  </div>
</template>

<script>
import axios from 'axios';
import { DxDateBox, DxDataGrid } from 'devextreme-vue';

export default {
  components: {
    DxDateBox,
    DxDataGrid
  },
  data() {
    return {
      from: null,
      to: null,
      gridData: []
    };
  },
  computed: {
    isValid() {
      return this.from && this.to;
    }
  },
  methods: {
    async search() {
      if (this.isValid) {
        const response = await axios.get('/api/authentications', {
          params: {
            from: this.from.toISOString(),
            to: this.to.toISOString()
          }
        });
        this.gridData = response.data;
      }
    }
  }
};
</script>

<style scoped>
.rounded-box {
  border: 1px solid #ccc;
  border-radius: 12px;
  padding: 10px;
  margin-bottom: 10px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.date-inputs {
  display: flex;
  gap: 10px;
  margin-bottom: 10px;  /* Optional: Adds some space between the date inputs and the search button */
}

.date-input {
  display: flex;
  flex-direction: column;
}
.global-image {
  z-index: 2;  
}
</style>
