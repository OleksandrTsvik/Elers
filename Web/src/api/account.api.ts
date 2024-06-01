import { api } from '.';

export const accountApi = api.injectEndpoints({
  overrideExisting: false,
  endpoints: (builder) => ({
    logout: builder.mutation<void, void>({
      query: () => ({
        url: '/auth/logout',
        method: 'POST',
        body: {},
      }),
      invalidatesTags: ['Session'],
    }),
  }),
});

export const { useLogoutMutation } = accountApi;
