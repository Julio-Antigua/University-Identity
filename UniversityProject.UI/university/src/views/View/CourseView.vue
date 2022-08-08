<template>

    <course></course>

</template>

<script>
import course from '../../components/View/CourseViewComponent.vue'
export default{
    name:'studentView',
    props:{
        msg:String
    },
    components:{
        course
    }
}

</script>

<style>

</style>