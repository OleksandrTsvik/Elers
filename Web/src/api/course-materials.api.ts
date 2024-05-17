import { coursesApi } from './courses.api';

interface SaveCourseMaterialRequest {
  tabId: string;
  text: string;
}

export const courseMaterialsApi = coursesApi.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    createCourseMaterial: builder.mutation<string, SaveCourseMaterialRequest>({
      query: ({ tabId, ...data }) => ({
        url: `/courseMaterials/${tabId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: ['Course'],
    }),
  }),
});

export const { useCreateCourseMaterialMutation } = courseMaterialsApi;
