import { useTranslation } from 'react-i18next';

import { OptionChoiceValue } from './base';

import type { ValidatorRule } from 'rc-field-form/lib/interface';

interface Rules {
  options: ValidatorRule[];
}

export default function useQuestionMultipleChoiceRules(): Rules {
  const { t } = useTranslation();

  return {
    options: [
      {
        validator: async (_, options?: Partial<OptionChoiceValue>[]) => {
          if (!options || !Array.isArray(options) || options.length < 3) {
            return Promise.reject(
              new Error(t('course_test.add_three_answers')),
            );
          }

          if (options.some((item) => !item || !item.option)) {
            return Promise.reject(
              new Error(t('course_test.enter_all_answers')),
            );
          }

          if (options.filter((item) => item.isCorrect).length <= 1) {
            return Promise.reject(
              new Error(t('course_test.choose_more_than_one_correct_answer')),
            );
          }

          return Promise.resolve();
        },
      },
    ],
  };
}
