# encoding: utf-8
# frozen_string_literal: true

# Moments version builder module
module JWT
  def self.gem_version
    Gem::Version.new VERSION::STRING
  end

  # Moments version builder module
  module VERSION
    # major version
    MAJOR = 1
    # minor version
    MINOR = 6
    # tiny version
    TINY  = 0
    # alpha, beta, etc. tag
    PRE   = 'dev'

    # Build version string
    STRING = [MAJOR, MINOR, TINY, PRE].compact.join('.')
  end
end
