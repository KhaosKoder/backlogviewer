<template><div>
  <GlobalImage/>
   <DxTabPanel>
        <DxItem title="Logs/Transactions" icon="money">
          <template #default>
          <div> <VikingTransactions/></div>
          </template>
        </DxItem>
        <DxItem title="Approve-IT" icon="checklist">
          <template #default>
          <div> 
              <VikingAuthentications/>
          </div>
          </template> 
        </DxItem>
    </DxTabPanel>
  </div>
</template>

<script>
import DxTabPanel, {DxItem} from 'devextreme-vue/tab-panel';
import VikingTransactions from "@/components/viking-transactions.vue";
import    VikingAuthentications from "@/components/viking-authentications.vue";
import GlobalImage from '@/components/GlobalImage.vue';

export default {
  components: {
    DxTabPanel,
    DxItem,
    VikingTransactions,
    VikingAuthentications,
    GlobalImage

  }
};
</script>
